﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MinAPI.Demo.Domain.Entities;

public partial class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoList> TodoList { get; set; }

    public virtual DbSet<UserProfile> UserProfile { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasKey(e => e.TodoId);

            entity.Property(e => e.TodoId)
                .HasDefaultValueSql("(newid())")
                .HasComment("PK");
            entity.Property(e => e.AddTime)
                .HasComment("新增時間")
                .HasColumnType("datetime");
            entity.Property(e => e.CompleteTime)
                .HasComment("完成時間")
                .HasColumnType("datetime");
            entity.Property(e => e.IsComplete)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasComment("是否完成 (Y:是 N:否)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1)
                .HasComment("姓名");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("標題");
            entity.Property(e => e.TodoContent)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("完成事項內容");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Account);

            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("帳號");
            entity.Property(e => e.AddTime)
                .HasComment("加入時間")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("密碼");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}