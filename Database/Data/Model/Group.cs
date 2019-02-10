using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Data.Model
{
   public class Group
    {
        internal class Configuration : IEntityTypeConfiguration<Group>
        {
            public void Configure(EntityTypeBuilder<Group> builder)
            {
                builder.HasKey(x => x.ID);
				builder.Property(x => x.Name).IsRequired();
				builder.Property(x => x.StartEducation).IsRequired().HasColumnType("datetime");
                builder.HasMany(x => x.Users).WithOne(x => x.Group);
                builder.HasMany(x => x.ScheduleSubjects).WithOne(x => x.Group);
                builder.HasIndex(x => x.Name);
            }
        }

        private int _id;
        private string _name;
        private string _inviteString;
        private ICollection<User> _users;
        private DateTime _startEducation;
        private ICollection<ScheduleField> _scheduleSubjects;

        protected Group() { }
        public Group(string name, DateTime startEducation, User user)
        {
            _name = name;
            _startEducation = startEducation;
            _users = new HashSet<User>() { user };
            _scheduleSubjects = new HashSet<ScheduleField>();
            //_inviteString = Guid.NewGuid().
        }

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string InviteString { get => _inviteString; set => _inviteString = value; }
        public virtual ICollection<User> Users { get => _users; set => _users = value; }
        public DateTime StartEducation { get => _startEducation; set => _startEducation = value; }
        public virtual ICollection<ScheduleField> ScheduleSubjects { get => _scheduleSubjects; set => _scheduleSubjects = value; }
        public bool Parity => (((DateTime.Now - _startEducation).Days + (int)_startEducation.DayOfWeek - 1) / 7) % 2 == 0 ? true : false;
    }
}
