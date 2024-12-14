namespace EvaluationSpaceAPI.DTOs
{
    public class StudentClassroomDTO
    {
        public string Name { get; set; }

        public List<UserDTO> Colleagues { get; set; }
        public List<UserDTO> Teachers { get; set; }
    }
}
