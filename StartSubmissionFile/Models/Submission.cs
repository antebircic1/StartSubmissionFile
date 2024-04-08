using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartSubmissionFile.Models
{
	internal class Submission
	{
		public int Id { get; set; }
		public int FarmId { get; set; }
		public int BarcodeId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsGenerated { get; set; }
        public DateTime? DateGenerated { get; set; }
        public string? Error { get; set; }
    }
}
