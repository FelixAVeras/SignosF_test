import { create } from "zustand";
import { User } from "../types";

type State = {
  selectedUser: User | null;
  setSelectedUser: (user: User) => void;
};

export const useUserStore = create<State>((set) => ({
  selectedUser: null,
  setSelectedUser: (user) => set({ selectedUser: user }),
}));
