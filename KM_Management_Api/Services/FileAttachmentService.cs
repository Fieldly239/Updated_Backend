using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface IFileAttachmentService
    {
        Task<IEnumerable<FileAttachment>> GetAll();
        Task<FileAttachment> GetById(int id);
        Task<bool> Add(FileAttachment model);
        Task<bool> Update(FileAttachment model);
        Task<bool> Delete(int id);
        Task<List<FileModel>> FileUpload(FileAttachment files);
        Task<string> GetFileInfo(int id);
        Task<IEnumerable<FileAttachment>> GetAllByKMId(string kmid);
    }

    public class FileAttachmentService : IFileAttachmentService
    {
        private readonly IFileAttachmentRepository fileAttachmentRepository;
        private IWebHostEnvironment hostingEnvironment;

        private const string FOLDERFILE_ATTACHMENT = "Attachment";
        public FileAttachmentService(
            IFileAttachmentRepository fileAttachmentRepository,
            IWebHostEnvironment hostingEnvironment
        )
        {
            this.fileAttachmentRepository = fileAttachmentRepository;
            this.hostingEnvironment = hostingEnvironment;

        }

        public async Task<IEnumerable<FileAttachment>> GetAll()
        {
            return await fileAttachmentRepository.GetAll();
        }

        public async Task<FileAttachment> GetById(int id)
        {
            return await fileAttachmentRepository.GetById(id);
        }

        public async Task<bool> Add(FileAttachment model)
        {
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            var res = await fileAttachmentRepository.Add(model);
            return res > 0;
        }

        public async Task<bool> Update(FileAttachment model)
        {
            var fileAttachment = await fileAttachmentRepository.GetById(model.Id);
            if (fileAttachment == null)
            {
                throw new Exception("Error Not Found");
            }
            model.ModifiedDate = DateTime.Now;
            var res = await fileAttachmentRepository.Update(model);
            return res > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var file = await fileAttachmentRepository.GetById(id);

            if (file == null)
            {
                throw new Exception("Error Not Found");
            }
            else
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, FOLDERFILE_ATTACHMENT, file.FK_KMId);
                if (File.Exists(Path.Combine(uploads, file.FileName + file.FileExtension)))
                {
                    File.Delete(Path.Combine(uploads, file.FileName + file.FileExtension));
                }
            }

            var res = await fileAttachmentRepository.Delete(id);
            return res > 0;
        }

        public async Task<List<FileModel>> FileUpload(FileAttachment files)
        {
            try
            {
                var fileAttachment = await fileAttachmentRepository.GetAll();

                if (files.FormFiles == null || files.FormFiles.Count == 0)
                {
                    throw new Exception("File not selected");
                }

                var uploads = Path.Combine(hostingEnvironment.WebRootPath, FOLDERFILE_ATTACHMENT, files.FK_KMId);

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var filePaths = new List<FileModel>();

                foreach (var file in files.FormFiles)
                {
                    if (file.Length > 0)
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                        FileModel fileModel = new FileModel();
                        fileModel.FileId = timestamp;
                        fileModel.FileName = Path.GetFileNameWithoutExtension(file.FileName);// + "_" + timestamp;
                        fileModel.FileExtension = Path.GetExtension(file.FileName);
                        filePaths.Add(fileModel);
                        var checkFiles = fileAttachment.Where(m => m.FileName.Equals(fileModel.FileName) && m.FileExtension.Equals(fileModel.FileExtension)).ToList();
                        if (checkFiles.Count() == 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, Path.GetFileNameWithoutExtension(file.FileName) + Path.GetExtension(file.FileName)), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                            files.FileName = fileModel.FileName;
                            files.FileExtension = fileModel.FileExtension;
                            files.CreatedDate = DateTime.Now;
                            files.ModifiedDate = DateTime.Now;
                            await fileAttachmentRepository.Add(files);
                        }

                    }
                }

                return filePaths;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> GetFileInfo(int id)
        {
            var _file = await fileAttachmentRepository.GetById(id);

            string fullPath = string.Empty;
            if (_file != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, FOLDERFILE_ATTACHMENT, _file.FK_KMId);
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(uploads, _file.FileName + _file.FileExtension)))
                {
                    // If file found, delete it    
                    fullPath = Path.Combine(FOLDERFILE_ATTACHMENT, _file.FK_KMId, _file.FileName + _file.FileExtension);
                    //fullPath = Path.Combine(uploads, _file.FileName + _file.FileExtension);
                }
            }
            return fullPath;
        }

        public async Task<IEnumerable<FileAttachment>> GetAllByKMId(string kmid)
        {
            return await fileAttachmentRepository.GetAllByKMId(kmid);
        }

    }
}
