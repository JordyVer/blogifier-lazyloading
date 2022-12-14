using Blogifier.Shared;

namespace LazyShared
{
    public class BlogStateProvider
    {
        public PostType PostType { get; set; } = PostType.Post;

        public event Action OnChange;

        public void SetPostType(PostType postType)
        {
            PostType = postType;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
