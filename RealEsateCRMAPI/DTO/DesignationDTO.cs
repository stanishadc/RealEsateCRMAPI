namespace RealEsateCRMAPI.DTO
{
    public class DesignationDTO
    {
        public int DesignationId { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public double Commission { get; set; }
        public int ParentId { get; set; }
    }
}
