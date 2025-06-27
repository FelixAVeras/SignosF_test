import { request, gql } from "graphql-request";

const endpoint = "https://graphqlzero.almansi.me/api";

export const fetchUserPosts = async (userId: string) => {
  const query = gql`
    query GetPosts($userId: ID!) {
      user(id: $userId) {
        posts {
          data {
            id
            title
            body
          }
        }
      }
    }
  `;
  const data = await request(endpoint, query, { userId }) as {
    user: {
      posts: {
        data: Array<{
          id: string;
          title: string;
          body: string;
        }>;
      };
    };
  };
  return data.user.posts.data;
};
