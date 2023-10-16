using NodaTime;
using System.Runtime.CompilerServices;

namespace ProjectManager.Data.Interfaces
{
    public interface ITrackable
    {
        public Instant CreateAt { get; set; }
        public string CreateBy { get; set; }
        public Instant ModifiedAt { get; set; }
        public string ModifiedBy { get; set;}
        public Instant? DeleteAt { get; set; }
        public string? DeleteBy { get; set; }

    }

    public static class ITreckableExtentions
    {
        private const string SYSTEM = "System";

        public static T SetCreateBySystem<T>(this T trackable, Instant now)
            where T : class, ITrackable
            => trackable.SetCreateBy(SYSTEM, now);

        public static T SetModifyBySystem<T>(this T trackable, Instant now)
            where T : class, ITrackable
            => trackable.SetModifyBy(SYSTEM, now);
        public static T SetDeleteBySystem<T>(this T trackable, Instant now)
            where T : class, ITrackable
            => trackable.SetDeleteBy(SYSTEM, now);

        public static T SetCreateBy<T>(this T trackable, string author, Instant now)
            where T : class, ITrackable
        {
            trackable.CreateAt = now;
            trackable.CreateBy = author;

            return trackable.SetModifyBy(author, now);

        }


        public static T SetModifyBy<T>(this T trackable, string author, Instant now)
             where T : class, ITrackable
        {
            trackable.ModifiedAt = now;
            trackable.ModifiedBy = author;

            return trackable;

        }
        public static T SetDeleteBy<T>(this T trackable, string author, Instant now)
             where T : class, ITrackable
        {
            trackable.DeleteAt = now;
            trackable.DeleteBy = author;

            return trackable;

        }

    }
}
