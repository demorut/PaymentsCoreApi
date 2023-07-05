using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Dtos
{
	public class UserDocumentDto
	{
        public string? UserId { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentExtension { get; set; }
        public string? DocumentPath { get; set; }
    }
}

