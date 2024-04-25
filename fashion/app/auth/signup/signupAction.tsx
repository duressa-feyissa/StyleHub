"use server";
import { sendVerificationCode } from "@/app/lib/actions";
import { redirect } from "next/navigation";
import { send } from "process";

export default async function signupAction(
  currentState: any,
  formData: FormData
): Promise<string> {
  // Get the data off the form
  const firstName = formData.get("firstName");
  const lastName = formData.get("lastName");
  const email = formData.get("email");
  const password = formData.get("password");

  //  Send to our api route
  const res = await fetch(
    `${process.env.ROOT_URL}/api/Authentication/Register`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        registeration: {
          firstName: firstName,
          lastName: lastName,
          email: email,
          password: password,
        },
      }),
    }
  );

  const json = await res.json();

  // Redirect to login if registration is success
  if (res.ok) {
    await sendVerificationCode(json.data.email);
    redirect("/auth/verify-email?email=" + json.data.email);
  } else {
    if (json.Message === "Email not verified") {
      await sendVerificationCode(email as string);
      redirect("/auth/verify-email?email=" + email);
    }
    return json.Message;
  }
}
