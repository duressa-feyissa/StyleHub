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

  return JSON.parse(session);
}

export async function sendVerificationCode(email: string) {
  const res = await fetch(
    `${process.env.ROOT_URL}/api/Authentication/Send-Verification-Email-Code`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
      }),
    }
  );

  return res.ok;
}

export async function verifyEmail(email: string, code: string) {
  const res = await fetch(
    `${process.env.ROOT_URL}/api/Authentication/Verify-Email`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
        code,
      }),
    }
  );

  if (res.ok) {
    const json = await res.json();
    redirect("/auth/login?email=" + json.data.email);
  } else {
    return "Invalid code. Please try again.";
  }
}

export async function sendPasswordResetCode(email: string) {
  const res = await fetch(
    `${process.env.ROOT_URL}/api/Authentication/Send-Reset-Password-Code`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
      }),
    }
  );

  return res.ok;
}

export async function resetPassword(
  email: string,
  code: string,
  password: string
) {
  const res = await fetch(
    `${process.env.ROOT_URL}/api/Authentication/Reset-Password`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
        code,
        password,
      }),
    }
  );

  return res.ok;
}
