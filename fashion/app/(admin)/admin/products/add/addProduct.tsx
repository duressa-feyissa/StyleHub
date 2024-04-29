"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import Image from "next/image";
import { Upload } from "lucide-react";
import { z } from "zod";

import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { Checkbox } from "@/components/ui/checkbox";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { ChangeEvent, useState } from "react";
import { toBase64 } from "@/lib/utils";
import MultiSelectFormField from "@/components/ui/multi-select";
import { BrandType, FilterType, LocationType } from "@/lib/type";
import { productAddformSchema } from "@/lib/formSchema";
import { addProduct } from "@/server/actions/add-product";
import { useAction } from "next-safe-action/hooks";
import { useGetFilters } from "@/lib/data/get-filters";

async function getImageData(event: ChangeEvent<HTMLInputElement>) {
  if (!event.target.files || event.target.files.length <= 0)
    return { files: [], displayUrl: "", id: "", imageUrl: "" };

  // FileList is immutable, so we need to create a new one
  const dataTransfer = new DataTransfer();

  // Add newly uploaded images
  Array.from(event.target.files!).forEach((image) => {
    dataTransfer.items.add(image);
  });

  const files = dataTransfer.files;
  const displayUrl = URL.createObjectURL(event.target.files![0]);

  console.log(files, displayUrl);

  const file = event?.target.files[0];
  const base64Image = (await toBase64(file)) as string;
  const response = await fetch(`/api/image`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      base64Image,
    }),
  });

  const result = await response.json();
  console.log(result);

  return {
    files,
    displayUrl,
    id: result?.data?.id,
    imageUrl: result?.data?.imageUrl,
  };
}

export default function AddProduct() {
  const { data: filters, error: postError, fetchStatus } = useGetFilters();
  const [images, setImages] = useState<Record<string, string>>({});

  const form = useForm<z.infer<typeof productAddformSchema>>({
    resolver: zodResolver(productAddformSchema),
    defaultValues: {
      title: "",
      description: "",
      price: 0,
      target: "",
      quantity: 0,
      condition: "",
      isNegotiable: true,
      isPublished: true,
      city: "",
      latitude: 0,
      longitude: 0,
      brandId: "",
      imageIds: [],
      categoryIds: [],
      sizeIds: [],
      colorIds: [],
      materialIds: [],
    },
  });

  const { execute, status } = useAction(addProduct, {
    onSuccess(data) {
      if (data?.error) console.log(data.error);
      if (data?.success) console.log(data.success);
    },
    onExecute(data) {
      console.log("creating post....");
    },
  });

  function onSubmit(values: z.infer<typeof productAddformSchema>) {
    execute(values);
    // form.reset();
  }

  if (postError || !filters) return postError?.message;

  const { brands, categories, colors, locations, materials, sizes } = filters;

  // const onSubmit = async (data: z.infer<typeof productAddformSchema>) => {
  //   console.log("data", data);
  // const response = await fetch("http://localhost:3000" + `/api/products`, {
  //   method: "POST",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify(data),
  // });

  // console.log("response", response);
  // const result = await response.json();
  // console.log("result", result);
  // };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <div className="grid gap-4 md:grid-cols-[1fr_250px] lg:grid-cols-3 lg:gap-8">
          <div className="grid auto-rows-max items-start gap-4 lg:col-span-2 lg:gap-8">
            <Card x-chunk="dashboard-07-chunk-0">
              <CardHeader>
                <CardTitle>Product Details</CardTitle>
                <CardDescription>
                  Describe your product in detail to attract customers and
                  improve SEO results
                </CardDescription>
              </CardHeader>
              <CardContent>
                <div className="grid gap-6">
                  <div className="grid gap-3">
                    <FormField
                      control={form.control}
                      name="title"
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Product Name</FormLabel>
                          <FormControl>
                            <Input
                              id="title"
                              type="text"
                              className="w-full"
                              placeholder="Gamer Gear Pro Controller"
                              {...field}
                            />
                          </FormControl>
                          <FormDescription />
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                  <div className="grid gap-3">
                    <FormField
                      control={form.control}
                      name="description"
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Description</FormLabel>
                          <FormControl>
                            <Textarea
                              id="description"
                              placeholder="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam auctor, nisl nec ultricies ultricies, nunc nisl ultricies nunc, nec ultricies nunc nisl nec nunc."
                              className="min-h-32"
                              {...field}
                            />
                          </FormControl>
                          <FormDescription />
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                  <div className="grid gap-6 grid-cols-2">
                    <div className="grid gap-3">
                      <FormField
                        control={form.control}
                        name="price"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Price</FormLabel>
                            <FormControl>
                              <Input
                                id="price"
                                type="number"
                                placeholder="99.99"
                                {...field}
                              />
                            </FormControl>
                            <FormDescription />
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="grid gap-3">
                      <FormField
                        control={form.control}
                        name="quantity"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Quantity</FormLabel>
                            <FormControl>
                              <Input
                                id="quantity"
                                type="number"
                                placeholder="200"
                                {...field}
                              />
                            </FormControl>
                            <FormDescription />
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                  </div>
                  <div className="grid gap-6 grid-cols-2">
                    <div className="grid gap-3">
                      <FormField
                        control={form.control}
                        name="target"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Target</FormLabel>
                            <Select
                              onValueChange={field.onChange}
                              defaultValue={field.value}
                            >
                              <FormControl>
                                <SelectTrigger
                                  {...field}
                                  id="target"
                                  aria-label="Select target"
                                >
                                  <SelectValue placeholder="Select target" />
                                </SelectTrigger>
                              </FormControl>
                              <SelectContent>
                                <SelectItem value="all">All</SelectItem>
                                <SelectItem value="men">Men</SelectItem>
                                <SelectItem value="women">Women</SelectItem>
                                <SelectItem value="children">
                                  Children
                                </SelectItem>
                              </SelectContent>
                            </Select>
                            <FormDescription />
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="grid gap-3">
                      <FormField
                        control={form.control}
                        name="condition"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Condition</FormLabel>
                            <Select
                              onValueChange={field.onChange}
                              defaultValue={field.value}
                            >
                              <FormControl>
                                <SelectTrigger
                                  {...field}
                                  id="condition"
                                  aria-label="Select condition"
                                >
                                  <SelectValue placeholder="Select condition" />
                                </SelectTrigger>
                              </FormControl>
                              <SelectContent>
                                <SelectItem value="new">New</SelectItem>
                                <SelectItem value="open">Open</SelectItem>
                                <SelectItem value="used">Used</SelectItem>
                              </SelectContent>
                            </Select>
                            <FormDescription />
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                  </div>
                  <div className="grid gap-6 md:grid-cols-3">
                    <div className="grid gap-3">
                      <FormField
                        control={form.control}
                        name="city"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Location</FormLabel>
                            <Select
                              onValueChange={field.onChange}
                              defaultValue={field.value}
                            >
                              <FormControl>
                                <SelectTrigger
                                  {...field}
                                  id="city"
                                  aria-label="Select city"
                                >
                                  <SelectValue placeholder="Select city" />
                                </SelectTrigger>
                              </FormControl>
                              <SelectContent>
                                {locations.map((location: LocationType) => (
                                  <SelectItem
                                    key={location.id}
                                    value={location.id}
                                  >
                                    {location.name}
                                  </SelectItem>
                                ))}
                              </SelectContent>
                            </Select>
                            <FormDescription />
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="grid gap-3">
                      <FormField
                        control={form.control}
                        name="brandId"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Brand</FormLabel>
                            <Select
                              onValueChange={field.onChange}
                              defaultValue={field.value}
                            >
                              <FormControl>
                                <SelectTrigger
                                  {...field}
                                  id="brandId"
                                  aria-label="Select brand"
                                >
                                  <SelectValue placeholder="Select brand" />
                                </SelectTrigger>
                              </FormControl>
                              <SelectContent>
                                {brands.map((brand: BrandType) => (
                                  <SelectItem key={brand.id} value={brand.id}>
                                    {brand.name}
                                  </SelectItem>
                                ))}
                              </SelectContent>
                            </Select>
                            <FormDescription />
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="flex items-center space-x-2">
                      <FormField
                        control={form.control}
                        name="isNegotiable"
                        render={({ field }) => (
                          <FormItem className="flex flex-row items-start space-x-3 space-y-0 rounded-md border py-2 px-4 mt-5">
                            <FormControl>
                              <Checkbox
                                checked={field.value}
                                onCheckedChange={field.onChange}
                              />
                            </FormControl>
                            <div className="space-y-1 leading-none">
                              <FormLabel>Negotiable</FormLabel>
                              <FormDescription />
                            </div>
                          </FormItem>
                        )}
                      />
                    </div>
                  </div>
                </div>
              </CardContent>
            </Card>
            <Card x-chunk="dashboard-07-chunk-2">
              <CardHeader>
                <CardTitle>Product Category</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="grid gap-6 sm:grid-cols-2">
                  <div className="grid gap-3">
                    <FormField
                      control={form.control}
                      name="categoryIds"
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Category</FormLabel>
                          <FormControl>
                            <MultiSelectFormField
                              options={categories}
                              defaultValue={field.value}
                              onValueChange={field.onChange}
                              placeholder="Select options"
                              variant="inverted"
                            />
                          </FormControl>
                          <FormDescription />
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                  <div className="grid gap-3">
                    <FormField
                      control={form.control}
                      name="sizeIds"
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Size</FormLabel>
                          <FormControl>
                            <MultiSelectFormField
                              options={sizes}
                              defaultValue={field.value}
                              onValueChange={field.onChange}
                              placeholder="Select options"
                              variant="inverted"
                            />
                          </FormControl>
                          <FormDescription />
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                  <div className="grid gap-3">
                    <FormField
                      control={form.control}
                      name="colorIds"
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Color</FormLabel>
                          <FormControl>
                            <MultiSelectFormField
                              options={colors}
                              defaultValue={field.value}
                              onValueChange={field.onChange}
                              placeholder="Select options"
                              variant="inverted"
                            />
                          </FormControl>
                          <FormDescription />
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                  <div className="grid gap-3">
                    <FormField
                      control={form.control}
                      name="materialIds"
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Material</FormLabel>
                          <FormControl>
                            <MultiSelectFormField
                              options={materials}
                              defaultValue={field.value}
                              onValueChange={field.onChange}
                              placeholder="Select options"
                              variant="inverted"
                            />
                          </FormControl>
                          <FormDescription />
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                </div>
              </CardContent>
            </Card>
          </div>
          <div className="grid auto-rows-max items-start gap-4 lg:gap-8">
            <Card x-chunk="dashboard-07-chunk-3">
              <CardHeader>
                <CardTitle>Product Status</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="grid gap-6">
                  <div className="grid gap-3">
                    <Label htmlFor="status">Status</Label>
                    <Select>
                      <SelectTrigger id="status" aria-label="Select status">
                        <SelectValue placeholder="Select status" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="draft">Draft</SelectItem>
                        <SelectItem value="published">Active</SelectItem>
                        <SelectItem value="archived">Archived</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </div>
              </CardContent>
            </Card>
            <Card className="overflow-hidden" x-chunk="dashboard-07-chunk-4">
              <CardHeader>
                <CardTitle>Product Images</CardTitle>
                <CardDescription>
                  Lipsum dolor sit amet, consectetur adipiscing elit
                </CardDescription>
              </CardHeader>
              <CardContent>
                <div className="grid gap-2">
                  <Image
                    alt="Product image"
                    className="aspect-square w-full rounded-md "
                    height="300"
                    src={
                      images[`${form.watch("imageIds")[0]}`] || "/vercel.svg"
                    }
                    width="300"
                  />
                  <div className="grid grid-cols-3 gap-2">
                    <button>
                      <Image
                        alt="Product image"
                        className="aspect-square w-full rounded-md "
                        height="84"
                        src={
                          images[`${form.watch("imageIds")[1]}`] ||
                          "/vercel.svg"
                        }
                        width="84"
                      />
                    </button>
                    <button>
                      <Image
                        alt="Product image"
                        className="aspect-square w-full rounded-md "
                        height="84"
                        src={form.watch("imageIds")[2] || "/vercel.svg"}
                        width="84"
                      />
                    </button>
                    <button className="flex aspect-square w-full items-center justify-center rounded-md border border-dashed">
                      <Upload className="h-4 w-4 text-muted-foreground" />
                      <span className="sr-only">Upload</span>
                    </button>
                  </div>
                  <FormField
                    control={form.control}
                    name="imageIds"
                    render={({ field: { onChange, value, ...rest } }) => (
                      <>
                        <FormItem>
                          <FormLabel>Circle Image</FormLabel>
                          <FormControl>
                            <Input
                              type="file"
                              accept="image/*"
                              {...rest}
                              onChange={async (event) => {
                                const { files, displayUrl, id, imageUrl } =
                                  await getImageData(event);
                                setImages({ ...images, [id]: imageUrl });
                                console.log(
                                  "imageUrl",
                                  images[`${form.watch("imageIds")[0]}`]
                                );
                                onChange([...value, id]);
                              }}
                            />
                          </FormControl>
                          <FormDescription>
                            Choose best image that bring spirits to your circle.
                          </FormDescription>
                          <FormMessage />
                        </FormItem>
                      </>
                    )}
                  />
                </div>
              </CardContent>
            </Card>
            <Card x-chunk="dashboard-07-chunk-5">
              <CardHeader>
                <CardTitle>Archive Product</CardTitle>
                <CardDescription>
                  Lipsum dolor sit amet, consectetur adipiscing elit.
                </CardDescription>
              </CardHeader>
              <CardContent>
                <Button size="sm" variant="secondary">
                  Archive Product
                </Button>
              </CardContent>
            </Card>
          </div>
        </div>
        <div className="flex items-center justify-center gap-2 md:hidden">
          <Button variant="outline" size="sm">
            Discard
          </Button>
          <Button size="sm" type="submit">
            Save Product
          </Button>
        </div>
      </form>
    </Form>
  );
}
