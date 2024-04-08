using Azure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StartSubmissionFile.Infrastructure.Persistance;
using StartSubmissionFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartSubmissionFile.Services
{
	internal class SubmissionService
	{

		const string requestUri = "http://localhost:9108/api/payments/v1/Submission/StartSubmissionFile";

		internal static async Task StartSubmissionFile()
		{
			using var context = new SubmissionDbContext();

			try
			{
				var submissions = await context.Submission.Where(x => !x.IsGenerated).OrderBy(x=>x.Id).ToListAsync();

				Console.WriteLine($"Broj preostalih: {submissions.Count()}\n");

				const int batchSize = 4;
				int counter = 0;

				for (int i = 0; i < submissions.Count; i += batchSize)
				{
					
					var currentBatch = submissions.Skip(i).Take(batchSize);
					var tasks = currentBatch.Select(submission => ProcessSubmissionAsync(submission, counter+1)).ToArray();
					await Task.WhenAll(tasks);
					counter++;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
			}
			
		}
		static async Task ProcessSubmissionAsync(Submission submission, int count)
		{
			using var httpClient = new HttpClient();

			using var context = new SubmissionDbContext();

			var request = new
			{
				FarmId = submission.FarmId,
				BarcodeId = submission.BarcodeId,
				SubmissionId = submission.Id
			};
			var jsonRequest = JsonConvert.SerializeObject(request);
			var requestBody = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
			SubmissionResponseDto? response;
			try
			{
				var res = await httpClient.PostAsync(requestUri, requestBody);
				var jsonResponse = await res.Content.ReadAsStringAsync();
				response = !String.IsNullOrEmpty(jsonResponse) ? JsonConvert.DeserializeObject<SubmissionResponseDto>(jsonResponse) : null;
				if (res.IsSuccessStatusCode)
				{
					submission.IsGenerated = true;
					submission.DateGenerated = DateTime.Now;
				}
				else
				{
					submission.Error += DateTime.Now + JsonConvert.SerializeObject(response);
				}
			}
			catch (Exception ex)
			{
				submission.Error += DateTime.Now + JsonConvert.SerializeObject(ex.Message);
			}
			finally
			{
				context.Update(submission);
				await context.SaveChangesAsync();
				Console.WriteLine($"{count}. Odrađen: {submission.BarcodeId} {DateTime.Now}\n");
			}
		}
	}
}
