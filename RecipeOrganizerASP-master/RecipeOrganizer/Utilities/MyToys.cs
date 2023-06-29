namespace RecipeOrganizer.Utilities
{
    public static class MyToys
    {
        public static string getLastLogin(DateTime? date)
        {
            DateTime? lastLoginTime = date;
            string displayText = string.Empty;

            if (lastLoginTime != null)
            {
                TimeSpan? timeDiff = DateTime.Now - lastLoginTime.Value;

                if (timeDiff != null)
                {
                    if (timeDiff.Value.TotalSeconds < 1)
                    {
                        displayText = "Just now";
                    }
                    else if (timeDiff.Value.TotalSeconds < 60)
                    {
                        displayText = $"{Math.Floor(timeDiff.Value.TotalSeconds)} seconds ago";
                    }
                    else if (timeDiff.Value.TotalMinutes < 60)
                    {
                        displayText = $"{Math.Floor(timeDiff.Value.TotalMinutes)} minutes ago";
                    }
                    else if (timeDiff.Value.TotalHours < 24)
                    {
                        displayText = $"{Math.Floor(timeDiff.Value.TotalHours)} hours ago";
                    }
                    else
                    {
                        displayText = $"{Math.Floor(timeDiff.Value.TotalDays)} days ago";
                    }
                }
                else
                {
                    displayText = "No login last";
                }
            }
            else
            {
                displayText = "No login last ";
            }
            return displayText;
        }
    }
}
