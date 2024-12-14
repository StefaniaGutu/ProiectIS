namespace EvaluationSpaceAPI.DTOs
{
    public class UpdateClassroomDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Guid> StudentIds { get; set; }
    }
}
