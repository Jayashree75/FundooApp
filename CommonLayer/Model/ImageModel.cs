using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.Model
{
  public class ImageModel
  {
    public static string ImageAdd(IFormFile url)
    {
      var cloudinary = new Cloudinary(new CloudinaryDotNet.Account(
            "dcppmimth",
             "564861446881366",
            "SGoAPaosICxm05kxNy_haYbTPiA"));
      var file = url.FileName;
      var stream = url.OpenReadStream();
      ImageUploadResult result = cloudinary.Upload(new ImageUploadParams
      {
        File = new FileDescription(file,stream)
      });
      return result.SecureUri.AbsoluteUri;
    }    
  }
}
