import { z } from "zod";

export const UserSchema = z.object({
  id: z.number(),
  name: z.string(),
  username: z.string(),
  email: z.string().email(),
});

export type User = z.infer<typeof UserSchema>;

export const PostSchema = z.object({
  id: z.string(),
  title: z.string(),
  body: z.string(),
});

export type Post = z.infer<typeof PostSchema>;
