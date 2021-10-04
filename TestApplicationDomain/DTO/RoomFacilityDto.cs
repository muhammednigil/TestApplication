namespace TestApplicationDomain.DTO
{
    public class RoomFacilityDto : FacilityDto
    {
        public RoomDto Room { get; set; }
        public RoomFacilityTypeDto FacilityType { get; set; }
    }
}


