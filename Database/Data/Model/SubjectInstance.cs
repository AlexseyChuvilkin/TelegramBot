using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Data.Model
{
    public class SubjectInstance
    {
        internal sealed class Configuration : IEntityTypeConfiguration<SubjectInstance>
        {
            public void Configure(EntityTypeBuilder<SubjectInstance> builder)
            {
                builder.HasKey(x => x.ID);
                builder.Property(x => x.SubjectType).IsRequired();
                builder.Property(x => x.Audience).IsRequired();
                builder.Property(x => x.Teacher).IsRequired();
            }
        }

        protected SubjectInstance() { }

        private int _id;
        private SubjectType _subjectType;
        private string _audience;
        private string _teacher;
        private Subject _subject;
        private ScheduleField _scheduleSubject;

        public SubjectInstance(SubjectType subjectType, string audience, string teacher, Subject subject)
        {
            _subjectType = subjectType;
            _audience = audience ?? throw new ArgumentNullException(nameof(audience));
            _teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
            _subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }

        public int ID { get => _id; set => _id = value; }
        public SubjectType SubjectType { get => _subjectType; set => _subjectType = value; }
        public string Audience { get => _audience; set => _audience = value; }
        public string Teacher { get => _teacher; set => _teacher = value; }
        public Subject Subject { get => _subject; set => _subject = value; }
        public ScheduleField ScheduleSubject { get => _scheduleSubject; set => _scheduleSubject = value; }
    }
}
