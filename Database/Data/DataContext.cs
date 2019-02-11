using Database.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Database.Data
{
    public class DataContext : DbContext
    {
        private readonly DbSet<User> _users;
        private readonly DbSet<Group> _groups;
        private readonly DbSet<SubjectCall> _subjectCalls;
        private readonly DbSet<Subject> _subject;
        private readonly DbSet<SubjectInstance> _subjectInstance;
        private readonly DbSet<ScheduleField> _scheduleField;
        private readonly DbSet<ParityDependentScheduleSubject> _parityDependentScheduleSubject;
        private readonly DbSet<ParityIndependentScheduleSubject> _parityIndependentScheduleSubject;

        public DataContext()
        {
            _users = Set<User>();
            _subjectCalls = Set<SubjectCall>();
            _groups = Set<Group>();
            _subject = Set<Subject>(); 
            _subjectInstance = Set<SubjectInstance>(); 
            _scheduleField = Set<ScheduleField>();
            _parityDependentScheduleSubject = Set<ParityDependentScheduleSubject>(); 
            _parityIndependentScheduleSubject = Set<ParityIndependentScheduleSubject>();

            _subjectCalls.Load();
            _users.Load();
            _groups.Load();
            _subject.Load();
            _subjectInstance.Load();
            _scheduleField.Load();
            _parityDependentScheduleSubject.Load();
            _parityIndependentScheduleSubject.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Data Source=mysqlserver228.database.windows.net;Initial Catalog=TelegramBot;User ID=azureuser;Password=Azure1234567;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new User.Configuration());
            modelBuilder.ApplyConfiguration(new Group.Configuration());
            modelBuilder.ApplyConfiguration(new SubjectCall.Configuration());
            modelBuilder.ApplyConfiguration(new Subject.Configuration());
            modelBuilder.ApplyConfiguration(new SubjectInstance.Configuration());
            modelBuilder.ApplyConfiguration(new ScheduleField.Configuration());
            modelBuilder.ApplyConfiguration(new ParityDependentScheduleSubject.ParityDependentConfiguration());
            modelBuilder.ApplyConfiguration(new ParityIndependentScheduleSubject.ParityIndependentConfiguration());
        }

        public DbSet<User> Users => _users;
        public DbSet<Group> Groups => _groups;
        public DbSet<SubjectCall> SubjectCalls => _subjectCalls;
        public DbSet<Subject> Subjects => _subject;
        public DbSet<SubjectInstance> SubjectInstances => _subjectInstance;
        public DbSet<ScheduleField> ScheduleFields => _scheduleField;
        public DbSet<ParityDependentScheduleSubject> ParityDependentScheduleSubjects => _parityDependentScheduleSubject;
        public DbSet<ParityIndependentScheduleSubject> ParityIndependentScheduleSubjects => _parityIndependentScheduleSubject;

        public void CreateUser(int telegramID, long chatID, out User user)
        {
            Users.Add(user = new User(telegramID, chatID));
            SaveChanges(); 
        }
        public bool GetUserByTelegramId(int telegramID, out User user)
        {
            user = Users.FirstOrDefault(x => x.TelegramID == telegramID);
            return user == null ? false : true;
        }
        public void CreateGroup(string name, DateTime startEducation, User user, out Group group) => Groups.Add(group = new Group(name, startEducation, user));
        public Group GetGroupById(int id) => Groups.Include(x=> x.ScheduleSubjects).First(x => x.ID == id);
    }
}
