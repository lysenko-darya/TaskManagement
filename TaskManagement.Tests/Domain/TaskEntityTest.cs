using FluentAssertions;
using TaskManagement.Contracts.Enums;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Tests.Domain;
public class TaskEntityTest
{
    [Theory]
    [InlineData(Status.New)]
    [InlineData(Status.InProgress)]
    [InlineData(Status.Done)]
    public void SetStatus_From_New_Success(Status newStatus)
    {
        //Arrange    
        var task = new TaskEntity("Author", status: Status.New);
        //Act 
        task.SetStatus(newStatus);
        //Assert
        task.Status.Should().Be(newStatus);
    }

    [Theory]
    [InlineData(Status.New)]
    [InlineData(Status.InProgress)]
    [InlineData(Status.Done)]
    public void SetStatus_From_Done_Fail(Status newStatus)
    {
        //Arrange    
        var task = new TaskEntity("Author", status: Status.Done);
        //Act          
        var func = () => task.SetStatus(newStatus);
        //Assert
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void SetStatus_From_InProgress_To_New_Fail()
    {
        //Arrange    
        var task = new TaskEntity("Author", status: Status.InProgress);
        //Act          
        var func = () => task.SetStatus(Status.New);
        //Assert
        func.Should().Throw<DomainException>();
    }

    [Fact]
    public void SetStatus_From_InProgress_To_Done_Success()
    {
        //Arrange    
        var task = new TaskEntity("Author", status: Status.InProgress);
        //Act          
        task.SetStatus(Status.Done);
        //Assert
        task.Status.Should().Be(Status.Done);
    }
}
