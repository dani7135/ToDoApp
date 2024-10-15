namespace ToDoApp.Models
{
    public class Filters
    {
        public Filters(string filtersting)
        {
            FilterString = filtersting ?? "all-all-all";
            string[] filterParts = FilterString.Split('-');
            CategoryId = filterParts[0];
            DueAt = filterParts[1];
            StatusId = filterParts[2];

        }
        public string FilterString { get; }
        public string CategoryId { get; }
        public string DueAt { get; }
        public string StatusId { get; }
        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasDueAt => DueAt.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";
        public static Dictionary<string, string> DueFilterValues =>
            new Dictionary<string, string>
            {
                {"past", "Overdue"},
                {"today", "Due Today"},
                {"future", "Due in Future"}
            };

        public bool IsPast => DueAt.ToLower() == "past";
        public bool IsFuture => DueAt.ToLower() == "future";
        public bool IsToday => DueAt.ToLower() == "today";


    }
}
