import { useUserStore } from "../store/userStore";
import { useQuery } from "@tanstack/react-query";
import { fetchUserPosts } from "../api/graphql";

export default function UserDetails() {
  const { selectedUser } = useUserStore();

  const { data: posts, isLoading } = useQuery({
    queryKey: ["posts", selectedUser?.id],
    queryFn: () => fetchUserPosts(String(selectedUser?.id)),
    enabled: !!selectedUser,
  });

  if (!selectedUser) return <p className="text-primary">Selecciona un usuario</p>;

  return (
    <div className="p-4 border rounded shadow-sm">
      <h2 className="text-xl font-bold">{selectedUser.name}</h2>
      <p>{selectedUser.email}</p>

      <h3 className="mt-4 font-semibold">Posts:</h3>
      {isLoading ? (
        <p>Cargando posts...</p>
      ) : (
        <ul className="space-y-2 mt-2">
          {posts?.map((post: any) => (
            <li key={post.id}>
              <strong>{post.title}</strong>
              <p>{post.body}</p>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
