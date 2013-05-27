using System;
using System.IO;

namespace Smallcode.Net
{
    /// <summary>
    ///     �ļ�Ԫ���ݡ�
    ///     ����ʹ�����¼��ֹ��췽����
    ///     ����·����new FileItem("C:/temp.jpg");
    ///     �����ļ���new FileItem(new FileInfo("C:/temp.jpg"));
    ///     �ֽ�����new FileItem("abc.jpg", bytes);
    /// </summary>
    public class FileItem
    {
        private readonly FileInfo fileInfo;
        private byte[] content;
        private string fileName;
        private string mimeType;

        /// <summary>
        ///     ���ڱ����ļ��Ĺ�������
        /// </summary>
        /// <param name="fileInfo">�����ļ�</param>
        public FileItem(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
            {
                throw new ArgumentException("fileInfo is null or not exists!");
            }
            this.fileInfo = fileInfo;
        }

        /// <summary>
        ///     ���ڱ����ļ�ȫ·���Ĺ�������
        /// </summary>
        /// <param name="filePath">�����ļ�ȫ·��</param>
        public FileItem(string filePath)
            : this(new FileInfo(filePath))
        {
        }

        /// <summary>
        ///     �����ļ������ֽ����Ĺ�������
        /// </summary>
        /// <param name="fileName">�ļ����ƣ�����˳־û��ֽ���������ʱ���ļ�����</param>
        /// <param name="content">�ļ��ֽ���</param>
        public FileItem(string fileName, byte[] content)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (content == null || content.Length == 0) throw new ArgumentNullException("content");

            this.fileName = fileName;
            this.content = content;
        }

        /// <summary>
        ///     �����ļ������ֽ�����ý�����͵Ĺ�������
        /// </summary>
        /// <param name="fileName">�ļ���������˳־û��ֽ���������ʱ���ļ�����</param>
        /// <param name="content">�ļ��ֽ���</param>
        /// <param name="mimeType">ý������</param>
        public FileItem(String fileName, byte[] content, String mimeType)
            : this(fileName, content)
        {
            if (string.IsNullOrEmpty(mimeType)) throw new ArgumentNullException("mimeType");
            this.mimeType = mimeType;
        }

        public string GetFileName()
        {
            if (fileName == null && fileInfo != null && fileInfo.Exists)
            {
                fileName = fileInfo.FullName;
            }
            return fileName;
        }

        public string GetMimeType()
        {
            if (mimeType == null)
            {
                mimeType = GetMimeType(GetContent());
            }
            return mimeType;
        }

        public byte[] GetContent()
        {
            if (content == null && fileInfo != null && fileInfo.Exists)
            {
                using (Stream fileStream = fileInfo.OpenRead())
                {
                    content = new byte[fileStream.Length];
                    fileStream.Read(content, 0, content.Length);
                }
            }
            return content;
        }

        /// <summary>
        ///     ��ȡ�ļ�����ʵý�����͡�Ŀǰֻ֧��JPG, GIF, PNG, BMP����ͼƬ�ļ���
        /// </summary>
        /// <param name="fileData">�ļ��ֽ���</param>
        /// <returns>ý������</returns>
        public static string GetMimeType(byte[] fileData)
        {
            var suffix = GetFileSuffix(fileData);
            string mimeType;

            switch (suffix)
            {
                case "JPG":
                    mimeType = "image/pjpeg";
                    break;
                case "GIF":
                    mimeType = "image/gif";
                    break;
                case "PNG":
                    mimeType = "image/png";
                    break;
                case "BMP":
                    mimeType = "image/bmp";
                    break;
                default:
                    mimeType = "application/octet-stream";
                    break;
            }

            return mimeType;
        }

        /// <summary>
        ///     ��ȡ�ļ�����ʵ��׺����Ŀǰֻ֧��JPG, GIF, PNG, BMP����ͼƬ�ļ���
        /// </summary>
        /// <param name="fileData">�ļ��ֽ���</param>
        /// <returns>JPG, GIF, PNG or null</returns>
        public static string GetFileSuffix(byte[] fileData)
        {
            if (fileData == null || fileData.Length < 10)
            {
                return null;
            }

            if (fileData[0] == 'G' && fileData[1] == 'I' && fileData[2] == 'F')
            {
                return "GIF";
            }
            else if (fileData[1] == 'P' && fileData[2] == 'N' && fileData[3] == 'G')
            {
                return "PNG";
            }
            else if (fileData[6] == 'J' && fileData[7] == 'F' && fileData[8] == 'I' && fileData[9] == 'F')
            {
                return "JPG";
            }
            else if (fileData[0] == 'B' && fileData[1] == 'M')
            {
                return "BMP";
            }
            else
            {
                return null;
            }
        }
    }
}