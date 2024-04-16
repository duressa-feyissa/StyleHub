"use server";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

export async function logout() {
  // Destroy the session
  cookies().set("session", "", { expires: new Date(0) });
}

export async function getSession() {
  const session = cookies().get("session")?.value;
  if (!session) return null;
  console.log("Session", JSON.parse(session));

  return JSON.parse(session);
}
