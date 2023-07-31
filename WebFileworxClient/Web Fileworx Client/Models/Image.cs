namespace Web_Fileworx_Client.Models
{
    public class Image : News
    {
        public string ImagePath { get; set; } //6
        public Image(string[] addedImage) : base(addedImage)
        {
            ImagePath = addedImage[6];

            ImagePath=ImagePath.Replace("\r\n", ""); // Removes all occurrences of "\r\n"
            
        }
    }
}
