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
import { ArrowBigLeft, Loader2 } from "lucide-react";
import { Input } from "@/components/ui/input";
import { redirect, useSearchParams } from "next/navigation";
import { sendResetPasswordCodeAction } from "./resetPasswordAction";
import Link from "next/link";

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
    <div className="p-16">
      <div className="mx-auto flex w-full flex-col justify-start space-y-6 sm:w-[350px] p-5">
        <div className="flex flex-col space-y-2 text-center">
          <h1 className="text-2xl font-normal tracking-tight">
            Reset your password
          </h1>
          <p className="text-sm text-muted-foreground">
            Enter your email to reset your password
          </p>
        </div>
        <Form {...form}>
          <form action={dispatch} className="space-y-6">
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Email</FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      placeholder="example@gmail.com"
                      className="w-full"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            {errorMessage && (
              <p className="text-red-600 dark:text-blue-800">{errorMessage}</p>
            )}
            <VerifyButton />
            <p className="text-sm text-muted-foreground flex justify-start gap-2">
              <Link
                href="/login"
                className="text-primary hover:underline flex items-center"
              >
                <ArrowBigLeft />
                Back to login
              </Link>
            </p>
          </form>
        </Form>
      </div>
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
    <Button
      aria-disabled={pending}
      type="submit"
      onClick={handleClick}
      className="w-full"
    >
      {pending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
      Submit
    </Button>
  );
}
