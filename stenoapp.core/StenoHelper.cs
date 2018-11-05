using System.Drawing;

namespace stenoapp.core
{
    public class StenoHelper
    {
        public Image EncryptTextAndEmbedInImage(string text, Image srcImage, string encryptionKey)
        {
            string encrypted = StringCipher.Encrypt(text, encryptionKey);
            return SteganographyHelper.embedText(encrypted, new Bitmap(srcImage));
        }

        public string ExtractTextFromImageAndDecypt(Image srcImage, string encryptionKey)
        {
            return StringCipher.Decrypt(SteganographyHelper.extractText(new Bitmap(srcImage)), encryptionKey);
        }
    }
}
