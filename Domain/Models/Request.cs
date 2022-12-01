using System;

namespace PTOManager.Domain.Models;

    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string? Description { get; set; }
		public Boolean? Approved { get; set; }

	
    }

    