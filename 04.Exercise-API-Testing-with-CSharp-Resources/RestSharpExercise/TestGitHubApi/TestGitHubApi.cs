using RestSharpServices;
using System.Net;
using System.Reflection.Emit;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework.Internal;
using RestSharpServices.Models;
using System;

namespace TestGitHubApi
{
    public class TestGitHubApi
    {
        private GitHubApiClient client;
        private static int lastCreatedIssueNumber;
        private static long lastCreatedCommentId;

        [SetUp]
        public void Setup()
        {            
            var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            client = new GitHubApiClient("https://api.github.com/repos/testnakov/", "mariostoykov", token);
        }


        [Test, Order (1)]
        public void Test_GetAllIssuesFromARepo()
        {
            string repo = "test-nakov-repo";
            var issues = client.GetAllIssues(repo);

            Assert.That(issues, Has.Count.GreaterThan(1), "There should be more than one issue.");

            foreach (var issue in issues)
            {
                Assert.That(issue.Id, Is.GreaterThan(0), "Issue ID should be greater than 0.");
                Assert.That(issue.Number, Is.GreaterThan(0), "Issue Number should be greater than 0.");
                Assert.That(issue.Title, Is.Not.Empty, "Issue Title should not be empty.");
            }
        }

        [Test, Order (2)]
        public void Test_GetIssueByValidNumber()
        {
            // Arrange
            string repo = "test-nakov-repo";
            int issueNumber = 1;

            // Act
            var issue = client.GetIssueByNumber(repo, issueNumber);

            // Assert
            Assert.IsNotNull(issue, "The response should contain issue data.");
            Assert.That(issue.Id, Is.GreaterThan(0), "The Issue ID should be a positive integer.");
            Assert.That(issue.Number, Is.EqualTo(issueNumber), "The issue number should match the requested number.");
        }
        
        [Test, Order (3)]
        public void Test_GetAllLabelsForIssue()
        {
            // Arrange
            string repo = "test-nakov-repo";
            int issueNumber = 6;

            // Act
            var labels = client.GetAllLabelsForIssue(repo, issueNumber);

            // Assert
            Assert.That(labels.Count, Is.GreaterThan(0));

            foreach (var label in labels)
            {
                Assert.That(label.Id, Is.GreaterThan(0), "Label ID should be greater than 0.");
                Assert.That(label.Name, Is.Not.Empty, "Label Name should not be empty.");

                Console.WriteLine("Label: " + label.Id + " - Name: " + label.Name);
            }
        }

        [Test, Order (4)]
        public void Test_GetAllCommentsForIssue()
        {
            // Arrange
            string repo = "test-nakov-repo";
            int issueNumber = 6;

            // Act
            var comments = client.GetAllCommentsForIssue(repo, issueNumber);

            // Assert
            Assert.That(comments.Count, Is.GreaterThan(0));

            foreach (var comment in comments)
            {
                Assert.That(comment.Id, Is.GreaterThan(0), "Comment ID should be greater than 0.");
                Assert.That(comment.Body, Is.Not.Empty, "Comment Body should not be empty.");

                Console.WriteLine("Comment: " + comment.Id + " - Name: " + comment.Body);
            }
        }

        [Test, Order(5)]
        public void Test_CreateGitHubIssue()
        {
            // Arrange
            string repo = "test-nakov-repo";
            string expectedTitle = "Test Title";
            string body = "Test Body";
            
            // Act
            var issue = client.CreateIssue(repo, expectedTitle, body);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(issue.Id, Is.GreaterThan(0));
                Assert.That(issue.Number, Is.GreaterThan(0));
                Assert.That(issue.Title, Is.Not.Empty);
                Assert.That(issue.Title, Is.EqualTo(expectedTitle));
            });

            Console.WriteLine(issue.Number);
            lastCreatedIssueNumber = issue.Number;
        }

        [Test, Order (6)]
        public void Test_CreateCommentOnGitHubIssue()
        {
            // Arrange
            string repo = "test-nakov-repo";
            int issueNumber = lastCreatedIssueNumber;
            string expectedBody = "Let me see";

            // Act
            var comment = client.CreateCommentOnGitHubIssue(repo, issueNumber, expectedBody);

            // Assert
            Assert.That(comment.Body, Is.EqualTo(expectedBody));
            Console.WriteLine(comment.Id);
            lastCreatedCommentId = comment.Id;
        }

        [Test, Order (7)]
        public void Test_GetCommentById()
        {
            // Arrange
            string repo = "test-nakov-repo";
            long commentId = lastCreatedCommentId;

            // Act
            Comment comment = client.GetCommentById(repo, commentId);

            // Assert
            Assert.IsNotNull(comment, "Expect to retrieve a comment, but got null.");
            Assert.That(comment.Id, Is.EqualTo(commentId), "The retrieved comment ID should match the requested comment ID.");
        }


        [Test, Order (8)]
        public void Test_EditCommentOnGitHubIssue()
        {
            // Arrange
            string repo = "test-nakov-repo";
            long commentId = lastCreatedCommentId;
            string newBody = "Updated Text";

            // Act
            var updatedComment = client.EditCommentOnGitHubIssue(repo, commentId, newBody);

            // Assert
            Assert.IsNotNull(updatedComment, "The updated comment should not be null.");
            Assert.That(updatedComment.Id, Is.EqualTo(commentId), "The updated comment ID should match the original comment ID.");
            Assert.That(updatedComment.Body, Is.EqualTo(newBody), "The updated comment text should match the new body text.");
        }

        [Test, Order (9)]
        public void Test_DeleteCommentOnGitHubIssue()
        {
            // Arrange
            string repo = "test-nakov-repo";
            long commentId = lastCreatedCommentId;

            // Act
            bool result = client.DeleteCommentOnGitHubIssue(repo, commentId);

            // Assert
            Assert.IsTrue(result, "The comment should be successfully deleted.");
        }
    }
}

