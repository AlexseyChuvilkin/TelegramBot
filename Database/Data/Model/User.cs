using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Data.Model
{
   public class User
    {
        internal class Configuration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasKey(x => x.ID);
				builder.Property(x => x.TelegramID).IsRequired();
				builder.Property(x => x.ChatID).IsRequired();
                builder.HasIndex(x => x.TelegramID).IsUnique();
                builder.HasOne(x => x.Group).WithMany(x => x.Users);
            }
        }

        private int _id;
        private int _telegramId;
        private long _chatId;
        private Group _group;

        protected User() { }
        public User(int telegramID, long chatID)
        {
            _telegramId = telegramID;
            _chatId = chatID;
        }

        public int ID { get => _id; set => _id = value; }
        public int TelegramID { get => _telegramId; set => _telegramId = value; }
        public long ChatID { get => _chatId; set => _chatId = value; }
        public virtual Group Group { get => _group; set => _group = value; }
    }
}
