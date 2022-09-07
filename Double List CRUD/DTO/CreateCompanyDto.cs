using Double_List_CRUD.Model;

namespace Double_List_CRUD.DTO
{
    public class CreateCompanyDto
    {
        public string? cName { get; set; }
        public string? cAddress { get; set; }
        public List<Employee> employee { get; set; }
    }
}
