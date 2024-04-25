"use server";
import { sendVerificationCode } from "@/lib/actions";
import { redirect } from "next/navigation";

export default async function verifyAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form

  const email = formData.get("email");
  const code = formData.get("pin");

  //  Send to our api route
  const res = await fetch(
    `${process.env.BACKEND_SERVER_URL}/api/Authentication/Verify-Email`,
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

  const json = await res.json();
  console.log(json);
  if (res.ok) {
    redirect("/auth/login");
  } else {
    return "Invalid code. Please try again.";
  }
}
