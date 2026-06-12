namespace CURE.Application.DTOs.PatientFile
{
    public class CreatePatientFilesDto
    {
        public long PatientId { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FilePath { get; set; }
    }
}
