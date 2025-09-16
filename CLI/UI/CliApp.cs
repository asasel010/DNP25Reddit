using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using InMemoryRepositories;

namespace CLI.UI;

public class CliApp
{
    IUserRepository userRepository;
    ICommentRepository commentRepository;
    IPostRepository postRepository;
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Welcome to Karachan 2.0");
        while (true)
        {
            Console.WriteLine("\r\nChoose an option: ");
            Console.WriteLine("1-Add new user, 2-Add new post, 3-Add new comment, 4-View all posts, 5-View posts");
            switch (Console.ReadLine())
            {
                case "1":
                    CreateUserView createUserView = new CreateUserView(userRepository);
                    Console.WriteLine("Username: ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    string password = Console.ReadLine();
                    await createUserView.AddUserAsync(username, password);
                    break;
                case "2":
                    CreatePostView createPostView = new CreatePostView(postRepository);
                    Console.WriteLine("Select user id (protologin): ");
                    int postUserId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Post title: ");
                    string postTitle = Console.ReadLine();
                    Console.WriteLine("Post content: ");
                    string postBody = Console.ReadLine();
                    await createPostView.AddPostAsync(postUserId, postTitle, postBody);
                    break;
                case "3":
                    CreateCommentView createCommentView = new CreateCommentView(commentRepository);
                    Console.WriteLine("Select user id (protologin): ");
                    int commentUserId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Select post id (cavemantype): ");
                    int commentPostId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Comment content: ");
                    string commentBody = Console.ReadLine();
                    await createCommentView.AddCommentAsync(commentUserId, commentPostId, commentBody);
                    break;
                case "4":
                    ListPostsView listPostsView = new ListPostsView(postRepository);
                    IQueryable<Post> posts = await listPostsView.ListPostsAsync();
                    Console.WriteLine("Displaying all posts: ");
                    foreach (var post in posts)
                    {
                        Console.WriteLine($"{post.Title} - {post.Id}");
                    }
                    break;
                case "5":
                    SinglePostView singlePostView = new SinglePostView(postRepository, commentRepository);
                    Console.WriteLine("Select id of post to view: ");
                    int postId = int.Parse(Console.ReadLine());
                    Post singlePost = await singlePostView.SinglePostAsync(postId);
                    
                    Console.WriteLine(singlePost.Title);
                    Console.WriteLine(singlePost.Body);
        
                    Console.WriteLine("Comments:");
                    IQueryable<Comment> comments = await singlePostView.ListCommentsAsync();
                    foreach (Comment comment in comments)
                    {
                        if (comment.PostId == postId)
                        {
                            Console.WriteLine($">{comment.Body}");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("THAT WAS A MISSINPUT");
                    break;
            }
        }
    }
}