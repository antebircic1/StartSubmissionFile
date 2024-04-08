using StartSubmissionFile.Services;
internal class Program
{
	static async Task Main(string[] args)
	{
		await SubmissionService.StartSubmissionFile();
	}
}