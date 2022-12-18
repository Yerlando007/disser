using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF.GOST;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Linq;
using System.Text;

namespace disser.Services
{
    public class GOSTService : IGOST
    {
        private readonly AppDbContext _db;
        public GOSTService(AppDbContext db)
            => _db = db;
        private static string _pathToImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//GOSTS");
        public SimilarFile Keywords(AllGOST Gosts)
        {
            var similarFiles = new SimilarFile();
            var result = new List<string>();
            var files = GetFiles();
            var keywordsRepeatByFileName = new Dictionary<string, int>();
            if (Gosts != null && Gosts.GOSTName.Length > 0)
            {
                foreach (var fileName in files)
                {
                    int counter = 0;
                    if (fileName != Gosts.GOSTName)
                    {
                        var fileNameDB = _db.AllGOST.FirstOrDefault(r => r.GOSTName == fileName);
                        if (fileNameDB != null)
                        {
                            foreach (var word_1 in fileNameDB.KeyWords.Split(','))
                            {
                                foreach (var word_2 in Gosts.KeyWords.Split(','))
                                {
                                    if (word_1 == word_2)
                                        counter++;
                                }
                            }
                            keywordsRepeatByFileName.Add(fileName, counter);
                        }
                    }
                }
                foreach (var fileName in keywordsRepeatByFileName.Keys)
                {
                    if (keywordsRepeatByFileName[fileName] >= 10)
                        result.Add(fileName);
                }
                similarFiles.GOSTName = Gosts.GOSTName;
                similarFiles.SimilarFiles = String.Join(',', result);
            }
            return similarFiles;
        }
        public List<string> _AddFiles(List<IFormFile> addedFiles)
        {
            var filelist = new List<string>();
            if (addedFiles.Count > 0)
            {
                foreach (var file in addedFiles)
                {
                    string filename = file.FileName;
                    filename = Path.GetFileName(filename);
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//GOSTS", filename);
                    if (!File.Exists(uploadpath))
                    {
                        var stream = new FileStream(uploadpath, FileMode.Create);
                        file.CopyToAsync(stream);
                        Thread.Sleep(1500);
                        filelist.Add(filename);
                        stream.Close();
                    }
                }
            }
            return filelist;
        }
        private List<string> GetFiles()
        {
            var dir = new DirectoryInfo(_pathToImages);
            var extensions = new string[] { "*.doc", "*.docx", "*.pdf" };
            var files = extensions.SelectMany(ext => dir.GetFiles(ext)).ToArray();

            return files.Select(file => file.Name).Distinct().ToList();
        }
        // Добавляет ГОСТы в базу из папки
        public async Task<List<AllGOST>> AddGOSTS()
        {
            var files = GetFiles();
            var documentRepository = new DocumentRepository();

            foreach (var fileName in files)
            {
                string filePath = $"{_pathToImages}/{fileName}";
                var repeatingWords = documentRepository.GetRepeatingWords(filePath, 3);
                await _db.AllGOST.AddAsync(new AllGOST
                {
                    GOSTName = fileName,
                    KeyWords = String.Join(",", repeatingWords.Where(x => repeatingWords.Values.OrderByDescending(i => i).Take(50).Contains(x.Value)).Select(p => p.Key))
                });
            }
            await _db.SaveChangesAsync();
            return await _db.AllGOST.ToListAsync();
        }
        //Добавляет файлы самого руководителя, с которым будет работать исполнитель
        public List<string> _AddRukovoditelFilesForGOST(List<IFormFile> addedFiles)
        {
            var filelist = new List<string>();
            if (addedFiles.Count > 0)
            {
                foreach (var file in addedFiles)
                {
                    string filename = file.FileName;
                    filename = Path.GetFileName(filename);
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//RukovoditelFiles", filename);
                    var stream = new FileStream(uploadpath, FileMode.Create);
                    file.CopyToAsync(stream);
                    Thread.Sleep(1500);
                    filelist.Add(filename);
                    stream.Close();
                }
            }
            return filelist;
        }
        //Добавляет файлы самого исполнителя, который он выполнил
        public string _AddIspolnitelFilesForGOST(IFormFile addedFile)
        {
            string filename = addedFile.FileName;
            filename = Path.GetFileName(filename);
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//IspolnitelFiles", filename);
            var stream = new FileStream(uploadpath, FileMode.Create);
            addedFile.CopyToAsync(stream);
            Thread.Sleep(1500);
            stream.Close();
            return filename;
        }
        //Добавляет файлы самого исполнителя, который он выполнил
        public string _AddTranslatorFilesForGOST(IFormFile addedFile)
        {
            string filename = addedFile.FileName;
            filename = Path.GetFileName(filename);
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//TranslatorFiles", filename);
            var stream = new FileStream(uploadpath, FileMode.Create);
            addedFile.CopyToAsync(stream);
            Thread.Sleep(1500);
            stream.Close();
            return filename;
        }
        //Выбор руководителя создателем
        public async Task<CreatedGOST> ChoiseRukovoditel([FromForm] ChoisedRukovoditel choisedRukovoditel)
        {
            var createdGOST = await _db.CreatedGOST.FirstOrDefaultAsync(r => r.Id == choisedRukovoditel.CreatedGOSTId);
            createdGOST.ChoisedRukovoditelID = choisedRukovoditel.RukovoditelId;
            createdGOST.OnWork = true;
            await _db.SaveChangesAsync();
            return createdGOST;
        }
        //Руководитель дает задание исполнителю
        public async Task<RukovoditelWantWork> GiveTasktoIspoknitel([FromForm] GiveTasktoIspolnitel giveTasktoIspolnitel)
        {
            var rukovoditelWantWorkRequest = await _db.RukovoditelWantWork.FirstOrDefaultAsync(r => r.Id == giveTasktoIspolnitel.TaskId);
            rukovoditelWantWorkRequest.IspolnitelId = giveTasktoIspolnitel.IspoltitelID;
            await _db.SaveChangesAsync();
            return rukovoditelWantWorkRequest;
        }
        //Исполнитель загружает свою работу
        public async Task<RukovoditelWantWork> IspolnitelWork([FromForm] IspolnitelWork ispolnitelWork, string userName)
        {
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == userName);
            var rukovoditelWantWorkRequest = await _db.RukovoditelWantWork.FirstOrDefaultAsync(r => r.IspolnitelId == userRequest.Id);
            var createdGOSTRequest = await _db.CreatedGOST.FirstOrDefaultAsync(r => r.Id == rukovoditelWantWorkRequest.CreatedGOSTId);             
            var ispolnitelFile = _AddIspolnitelFilesForGOST(ispolnitelWork.IspolnitelFile);
            rukovoditelWantWorkRequest.IspolnitelFile = ispolnitelFile;
            rukovoditelWantWorkRequest.CommentFromIspolnitel = ispolnitelWork.CommentFromIspolnitel;
            await _db.SaveChangesAsync();
            return rukovoditelWantWorkRequest;
        }
        //Руководитель принимает работу переводчика
        public async Task<TranslateFile> AccepTranslatorWork([FromForm] AcceptTanslatorWork acceptTanslatorWork)
        {
            var translateResult = new List<TranslateFile>();
            var translateFileRequest = await _db.TranslateFile.FirstOrDefaultAsync(r => r.TranslatorId == acceptTanslatorWork.TranslatorId);
            translateFileRequest.IsFinished = acceptTanslatorWork.IsFinished;
            translateFileRequest.CommentToTranslator = acceptTanslatorWork.CommentToTranslator;
            await _db.SaveChangesAsync();
            return translateFileRequest;
        }
        //Переводчики добавляют файл
        public async Task<List<TranslateFile>> TranslatorAddFile([FromForm] TranslatorAddFile translatorAddFile, string userName)
        {
            var translateResult = new List<TranslateFile>();
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == userName);
            var createdGOSTRequest = await _db.CreatedGOST.FirstOrDefaultAsync(r => r.ChoisedRukovoditelID == userRequest.LeaderID);
            var translatorFile = _AddTranslatorFilesForGOST(translatorAddFile.TranslateFileName);
            //посчитать количество страниц объеденного файла и найти процентное соотношение с загружаемым файлом
            translateResult.Add(new TranslateFile
            {
                TranslateFileName = translatorFile,
                CommentFromTranslator = translatorAddFile.CommentFromTranslator,
                TranslatorId = userRequest.Id,
                IsFinished = false,       
                //WorkPercentage = 
            });
            createdGOSTRequest.FullFileToTranslate = translateResult;
            await _db.SaveChangesAsync();
            return translateResult;
        }
        //Руководитель принимает работу
        public async Task<CreatedGOST> RukovoditelAcceptWork([FromForm] RukovoditelAcceptWork rukovoditelAcceptWork)
        {
            var result = new CreatedGOST();
            var rukovoditelWantWorkRequest = await _db.RukovoditelWantWork.FirstOrDefaultAsync(r => r.Id == rukovoditelAcceptWork.TaskId);
            var createdGOSTRequest = await _db.CreatedGOST.FirstOrDefaultAsync(r => r.Id == rukovoditelWantWorkRequest.CreatedGOSTId);
            var WorkIsponitelFullCount = await _db.RukovoditelWantWork.ToListAsync();
            var WorkIsponitelFinishCount = WorkIsponitelFullCount.Where(r => r.isFinishedTask == true).ToList();            
            double percentByTask = 100 / WorkIsponitelFullCount.Count;
            double percentFinish = percentByTask * WorkIsponitelFinishCount.Count;
            rukovoditelWantWorkRequest.CommentToIspolnitel = rukovoditelAcceptWork.CommentToIspolnitel;
            rukovoditelWantWorkRequest.isFinishedTask = rukovoditelAcceptWork.IsFinished;                  
            createdGOSTRequest.WorkPercentage = percentFinish;
            //надо сюда объеденный файл
            //createdGOSTRequest.EndedFile
            await _db.SaveChangesAsync();
            createdGOSTRequest.RukovoditelWantWork = WorkIsponitelFullCount;
            return createdGOSTRequest;
        }
        //Руководитель оставляет заявку на работу
        public async Task<CreatedGOST> TakeGOSTsByRukovoditel([FromForm] TakeGOSTs takeGOSTs, string userName)
        {
            var result = new List<RukovoditelWantWork>();
            var createdGOSTRequest = await _db.CreatedGOST.FirstOrDefaultAsync(r => r.Id == takeGOSTs.GOSTId);
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == userName);
            var fileList = _AddRukovoditelFilesForGOST(takeGOSTs.File);
            for (int i = 0; i < takeGOSTs.File.Count; i++)
            {
                result.Add(new RukovoditelWantWork
                {
                    File = fileList[i],
                    Comment = takeGOSTs.Comment[i],
                    StartDate = takeGOSTs.StartDate[i],
                    EndDate = takeGOSTs.EndDate[i],
                    RukovoditelId = userRequest.Id,
                    isFinishedTask = false
                    
                });         
            }
            createdGOSTRequest.RukovoditelWantWork = result;
            await _db.SaveChangesAsync();
            return createdGOSTRequest;
        }
        //получить полную информацию
        public async Task<CreatedGOST> GetCreatedGOST(int id, string userName)
        {
            var createdGOSTRequest = await _db.CreatedGOST.FirstOrDefaultAsync(r => r.Id == id);
            var userRequest = await _db.Users.FirstOrDefaultAsync(r => r.Username == userName);
            var translatedFileRequest = await _db.TranslateFile.Where(r => r.CreatedGOSTId == id).ToListAsync();
            if (createdGOSTRequest.userId != userRequest.Id)
            {
                return null;
            }        
            var RukovoditelWantWorkRequest = await _db.RukovoditelWantWork.ToListAsync();
            var createdGOSTFiles = await _db.AllGOST.Where(r => r.CreatedGOSTId == id).ToListAsync();
            if (userRequest.Id == createdGOSTRequest.ChoisedRukovoditelID)
            {
                RukovoditelWantWorkRequest = RukovoditelWantWorkRequest.Where(r => r.RukovoditelId == userRequest.Id).ToList();
            }
            var similarGOSTRequest = await _db.SimilarFiles.Where(r => r.CreatedGOSTId == id).ToListAsync();
            createdGOSTRequest.RukovoditelWantWork = RukovoditelWantWorkRequest;
            createdGOSTRequest.similarGOSTs = similarGOSTRequest;
            createdGOSTRequest.CreatedGOSTAddedFiles = createdGOSTFiles;
            createdGOSTRequest.FullFileToTranslate = translatedFileRequest;
            await _db.SaveChangesAsync();
            return createdGOSTRequest;
        }
        public async Task<List<CreatedGOST>> CreateGOST([FromForm] GostFormData gost)
        {
            var result = new List<CreatedGOST>();
            var similarFiles = new List<SimilarFile>();
            var gostFiles = new List<string>();
            var createdGOSTSFiles = new List<AllGOST>();
            var similarFile = new List<string>();
            if (gost.Gost.Count > 0)
            {
                gostFiles = _AddFiles(gost.Gost);
            }
            var files = GetFiles();
            var documentRepository = new DocumentRepository();
            foreach (var fileName in files)
            {
                var isExist = await _db.AllGOST.FirstOrDefaultAsync(r => r.GOSTName == fileName);
                if (isExist == null)
                {
                    string filePath = $"{_pathToImages}/{fileName}";
                    var repeatingWords = documentRepository.GetRepeatingWords(filePath, 3);
                    var AddinAllGOSTS = new AllGOST
                    {
                        GOSTName = fileName,
                        KeyWords = String.Join(",", repeatingWords.Where(x => repeatingWords.Values.OrderByDescending(i => i).Take(50).Contains(x.Value)).Select(p => p.Key))
                    };
                    createdGOSTSFiles.Add(AddinAllGOSTS);
                }
            }
            foreach(var item in createdGOSTSFiles)
            {
                var similarFileName = Keywords(item);
                similarFiles.Add(similarFileName);
            }
            var newGost = new CreatedGOST
            {
                userId = gost.UserId,
                CreatedGOSTAddedFiles = createdGOSTSFiles,
                OnWork = false,
                similarGOSTs = similarFiles,
                isFinished = false,
                WorkPercentage = 0
            };
            await _db.CreatedGOST.AddAsync(newGost);

            await _db.SaveChangesAsync();
            result.Add(newGost);
            return result;
        }
    }
}
