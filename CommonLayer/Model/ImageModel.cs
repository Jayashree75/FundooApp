using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.Model
{
  public class ImageModel
  {
    public static string ImageAdd(string url)
    {
      var cloudinary = new Cloudinary(new CloudinaryDotNet.Account(
            "dcppmimth",
             "564861446881366",
            "SGoAPaosICxm05kxNy_haYbTPiA"));
      ImageUploadResult result = cloudinary.Upload(new ImageUploadParams
      {
        File = new FileDescription(url)
      });
      return result.SecureUri.AbsoluteUri;
    }    
  }
}
