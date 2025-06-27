import { useQuery } from "@tanstack/react-query";
import { fetchUsers } from "../api/rest";
import { useUserStore } from "../store/userStore";
import { UserSchema } from "../types";

export default function UserList({ filter }: { filter: string }) {
  const { setSelectedUser } = useUserStore();
  const { data: users, isLoading } = useQuery({
    queryKey: ["users"],
    queryFn: fetchUsers,
  });

  if (isLoading) return <p>Cargando usuarios...</p>;

  return (
    <ul className="list-group list-group-flush">
      {users
        .filter((u: any) =>
          u.name.toLowerCase().includes(filter.toLowerCase())
        )
        .map((u: any) => {
          const parsed = UserSchema.safeParse(u);
          if (!parsed.success) return null;

          return (
            <li
              key={u.id}
              onClick={() => setSelectedUser(parsed.data)}
              className="list-group-item list-group-item-action"
              style={{
                cursor: 'pointer',
              }}
            >
              {u.name} <i className="bi bi-chevron-compact-right float-end"></i>
            </li>
          );
        })}
    </ul>
  );
}
