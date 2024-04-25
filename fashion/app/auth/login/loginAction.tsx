"use server";

import { sendVerificationCode } from "@/app/lib/actions";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

export default async function loginAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form
  const email = formData.get("email");
  const password = formData.get("password");

  //  Send to our api route
  const res = await fetch(`${process.env.ROOT_URL}/api/Authentication/Login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      loginRequest: {
        email: email,
        password: password,
      },
    }),
  });

  const json = await res.json();

  // Redirect to login if success
  if (res.ok) {
    cookies().set("session", JSON.stringify(json?.data), {
      secure: true,
      httpOnly: true,
      expires: Date.now() + 24 * 60 * 60 * 1000 * 3,
      path: "/",
      sameSite: "strict",
    });

    redirect("/filter");
  } else {
    if(json.Message === "Email not verified") {
      await sendVerificationCode(email as string);
      redirect("/auth/verify-email?email=" + email);
    }
    return json.Message;
  }
}
