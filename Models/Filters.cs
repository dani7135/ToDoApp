namespace ToDoApp.Models
{
    public class Filters
    {
        public Filters(string filtersting)
        {
            FilterString = filtersting ?? "all-all-all";
            string[] filterParts = FilterString.Split('-');
            if (filterParts.Length > 1)
            {


                CategoryId = filterParts[0];
                Due = filterParts[1];
                StatusId = filterParts[2];
            }
        }
        public string FilterString { get; }
        public string CategoryId { get; }
        public string Due { get; }
        public string StatusId { get; }
        public bool HasCategory => CategoryId != null && CategoryId.ToLower() != "all";
        public bool HasDueAt => Due != null && Due.ToLower() != "all";
        public bool HasStatus =>StatusId != null && StatusId.ToLower() != "all";
        public static Dictionary<string, string> DueFilterValues =>
            new Dictionary<string, string>
            {
                {"past", "Overdue"},
                {"today", "Due Today"},
                {"future", "Due in Future"}
            };

        public bool IsPast => Due.ToLower() == "past";
        public bool IsFuture => Due.ToLower() == "future";
        public bool IsToday => Due.ToLower() == "today";


    }
}
