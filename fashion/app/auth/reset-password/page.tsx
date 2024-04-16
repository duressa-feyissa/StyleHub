"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { useFormState } from "react-dom";
import { useFormStatus } from "react-dom";
import { Loader2 } from "lucide-react";
import { Input } from "@/components/ui/input";
import { redirect, useSearchParams } from "next/navigation";
import { sendResetPasswordCodeAction } from "./resetPasswordAction";

const FormSchema = z.object({
  pin: z.string().min(4, {
    message: "Your one-time password must be 4 characters.",
  }),
  email: z.string(),
});

export default function InputOTPForm() {
  const [errorMessage, dispatch] = useFormState(
    sendResetPasswordCodeAction,
    undefined
  );
  const searchParams = useSearchParams();
  const email = searchParams.get("email") || "";

  const form = useForm<z.infer<typeof FormSchema>>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      email,
    },
  });

  return (
    <div className="flex p-32 justify-center items-center">
      <Form {...form}>
        <form action={dispatch} className="space-y-6">
          <FormField
            control={form.control}
            name="email"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Email</FormLabel>
                <FormControl>
                  <Input {...field} className="w-full" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          {errorMessage && (
            <p className="text-red-600 dark:text-blue-800">{errorMessage}</p>
          )}
          <VerifyButton />
        </form>
      </Form>
    </div>
  );
}

function VerifyButton() {
  const { pending } = useFormStatus();

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    if (pending) {
      event.preventDefault();
    }
  };

  return (
    <Button aria-disabled={pending} type="submit" onClick={handleClick}>
      {pending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
      Submit
    </Button>
  );
}
