"use server";
import { sendVerificationCode } from "@/lib/actions/user.actions";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

export default async function resetPasswordAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form

  const email = formData.get("email");
  const code = formData.get("pin");
  const password = formData.get("password") as string;
  const confirmPassword = formData.get("confirmPassword");

  if (password.length < 6) {
    return "Password must be at least 6 characters";
  }

  if (password !== confirmPassword) {
    return "Passwords do not match";
  }

  //  Send to our api route
  const res = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Authentication/Reset-Password`,
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

  const json = await res.json();
  console.log(json);
  // Redirect to login if success
  if (res.ok) {
    cookies().set("session", JSON.stringify(json), {
      secure: true,
      httpOnly: true,
      expires: Date.now() + 24 * 60 * 60 * 1000 * 3,
      path: "/",
      sameSite: "strict",
    });

    redirect("/filter");
  } else {
    return json.Message;
  }
}

export async function sendResetPasswordCodeAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form

  const email = formData.get("email");

  //  Send to our api route
  const res = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Authentication/Send-Reset-Password-Code`,
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

  const json = await res.json();
  console.log(json);
  if (res.ok) {
    redirect("/reset-password/verify?email=" + email);
  } else {
    return json.Message;
  }
}
