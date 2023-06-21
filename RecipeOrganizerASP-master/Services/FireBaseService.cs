using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class FireBaseService
	{
		private static string ApiKey = "AIzaSyB-4-AYXKKdtIMZ04WWO38cLec56loIAt0";
		private static string Bucket = "recipeorganizer-58fca.appspot.com";
		private static string AuthEmail = "recipeorganizert3@gmail.com";
		private static string AuthPassword = "recipeorganizer123";

		public static async Task<string> UploadImage(List<IFormFile> files)
		{
			var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

			// get authentication token
			var authResultTask = auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
			var authResult = await authResultTask;
			var token = authResult.FirebaseToken;


			string imageLink = "";
			FirebaseImageModel firebaseImageModel = new FirebaseImageModel();
			foreach (var file in files)
			{
				if (file.Length > 0)
				{
					firebaseImageModel.ImageFile = file;
					var stream = firebaseImageModel.ImageFile.OpenReadStream();
					//you can use CancellationTokenSource to cancel the upload midway
					var cancellation = new CancellationTokenSource();

					var result = await new FirebaseStorage(Bucket,
						 new FirebaseStorageOptions
						 {
							 AuthTokenAsyncFactory = () => Task.FromResult(token)
						 })
					   .Child("products")
					   .Child(firebaseImageModel.ImageFile.FileName)
					   .PutAsync(stream, cancellation.Token);

					cancellation.Cancel();
					try
					{
						imageLink += result + "ro";

					}
					catch (Exception)
					{

						throw;
					}
				}
			}
			return imageLink;

		}
	}
}
	
