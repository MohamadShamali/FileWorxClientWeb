using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Web_Fileworx_Client.Models
{
    public class NewsServices
    {
        private StreamReader? newsReader;
        private StreamWriter? newsWriter;
        public List<News> allNews = new List<News>();

        public News? SelectedNews { get; set; }


        public void RemoveImageTextFile(Image image)
        {
            if (File.Exists(image.textPath))
            {
                File.Delete(image.textPath);
            }
        }

        public void RemoveImageImage(Image image)
        {
            if (File.Exists(image.ImagePath))
            {
                File.Delete(image.ImagePath);
            }
        }

        public void RemoveNews(News news) 
        {
            if (File.Exists(news.textPath))
            {
                File.Delete(news.textPath);
            }
        }
        public void AddNews(News news)
        {
            FileStream fs = new FileStream($"{EditBeforeRun.newsFiles}" + $"\\{news.GUID}.txt", FileMode.Create, FileAccess.Write);
            newsWriter = new StreamWriter(fs);
            newsWriter.AutoFlush = true;
            newsWriter.WriteLine($"{news.Title}%%$$##{news.Description}%%$$##{news.Body}%%$$##{DateTime.Now.ToString()}%%$$##{news.GUID}%%$$##{news.Category}%%$$##{String.Empty}");
            newsWriter?.Close();
        }

        public void AddImage(Image image)
        {
            FileStream fs = new FileStream($"{EditBeforeRun.newsFiles}" + $"\\{image.GUID}.txt", FileMode.Create, FileAccess.Write);
            newsWriter = new StreamWriter(fs);
            newsWriter.AutoFlush = true;
            newsWriter.WriteLine($"{image.Title}%%$$##{image.Description}%%$$##{image.Body}%%$$##{DateTime.Now.ToString()}%%$$##{image.GUID}%%$$##{String.Empty}%%$$##{image.ImagePath}");
            newsWriter?.Close();
        }

        public void SetSelectedNews(News news)
        {
            SelectedNews = news;
        }

        public async Task DeleteFilesInFolder(string folderPath)
        {
                if (Directory.Exists(folderPath))
                {
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
        }

        public void addAllSavedNews()
        {
            allNews.Clear();
            string[] savedNewsFiles = Directory.GetFiles(EditBeforeRun.newsFiles);

            foreach (string file in savedNewsFiles)
            {
                readNewsTextFile(file);
            }
        }

        public void RefreshNews()
        {
            allNews.Clear();
            addAllSavedNews();
        }

        private void readNewsTextFile(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            newsReader = new StreamReader(fs);
            string newsRecord = newsReader.ReadToEnd();
            if (newsRecord != null)
            {
                string[] thisNews = newsRecord.Split(EditBeforeRun.Seperator, StringSplitOptions.None);
                string path = thisNews[6].Replace("\r\n", ""); // Removes all occurrences of "\r\n"
                if (File.Exists(path))
                {
                    Image newImage = new Image(thisNews);
                    News newIMG = newImage;
                    allNews.Add(newIMG);
                }

                else
                {
                    News newNews = new News(thisNews);
                    allNews.Add(newNews);
                }
            }
            newsReader?.Close();
        }

        public List<News> GetAllNews()
        {
            return allNews;
        }
    }
}
