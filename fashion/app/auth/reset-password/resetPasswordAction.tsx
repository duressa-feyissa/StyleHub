"use server";
import { sendVerificationCode } from "@/app/lib/actions";
import { redirect } from "next/navigation";

export default async function resetPasswordAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form

  const email = formData.get("email");
  const code = formData.get("pin");
  const password = formData.get("password");
  const confirmPassword = formData.get("confirmPassword");

  if (password !== confirmPassword) {
    return "Passwords do not match";
  }

  //  Send to our api route
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
        password
      }),
    }
  );

  const json = await res.json();
  console.log(json);
  if (res.ok) {
    redirect("/auth/login");
  } else {
    return "Invalid code. Please try again.";
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

  const json = await res.json();
  console.log(json);
  if (res.ok) {
    redirect("/auth/reset-password/verify?email=" + email);
  } else {
    return "Invalid code. Please try again.";
  }
}