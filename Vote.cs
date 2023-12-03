public class Vote
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public bool isLiked { get; set; }
    public int like { get; set; }
    public int dislike { get; set; }
    public string? Description { get; set; }
}