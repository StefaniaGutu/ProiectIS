using System;
using System.Collections.Generic;
using EvaluationSpaceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSpaceAPI.EFContext;

public partial class EvaluationSpaceDbContext : DbContext
{
    public EvaluationSpaceDbContext()
    {
    }

    public EvaluationSpaceDbContext(DbContextOptions<EvaluationSpaceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answer");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.IdQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_Question");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.ToTable("Classroom");

            entity.HasIndex(e => e.Name, "ClassroomName_Unique").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdQuestionTypeNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.IdQuestionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_QuestionType");

            entity.HasOne(d => d.IdQuizNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.IdQuiz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Quiz");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.ToTable("QuestionType");

            entity.HasIndex(e => e.Name, "QuestionTypeName_Unique").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.ToTable("Quiz");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Quiz_User");

            entity.HasMany(d => d.IdClassrooms).WithMany(p => p.IdQuizzes)
                .UsingEntity<Dictionary<string, object>>(
                    "QuizClassroom",
                    r => r.HasOne<Classroom>().WithMany()
                        .HasForeignKey("IdClassroom")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_QuizClassroom_Classroom"),
                    l => l.HasOne<Quiz>().WithMany()
                        .HasForeignKey("IdQuiz")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_QuizClassroom_Quiz"),
                    j =>
                    {
                        j.HasKey("IdQuiz", "IdClassroom");
                        j.ToTable("QuizClassroom");
                    });
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.ToTable("Result");

            entity.HasIndex(e => new { e.IdQuiz, e.IdStudent }, "QuizStudentResult_Unique").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdQuizNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.IdQuiz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Result_Quiz");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Result_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");

            entity.HasMany(d => d.Classrooms).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserClassroom",
                    r => r.HasOne<Classroom>().WithMany()
                        .HasForeignKey("IdClassroom")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserClassroom_Classroom"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserClassroom_User"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdClassroom");
                        j.ToTable("UserClassroom");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
