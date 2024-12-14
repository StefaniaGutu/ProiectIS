namespace EvaluationSpaceAPI.DTOs
{
    public class TeacherClassroomDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<UserDTO> Students { get; set; }
    }
}
