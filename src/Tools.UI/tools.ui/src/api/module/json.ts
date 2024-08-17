import request from "@/utils/axios";

interface GenerateCSharpClassInput {
  json: string;
  className: string;
}
export const GenerateCSharpClass = (input: GenerateCSharpClassInput) => {
  return request<Md5Output>({
    url: "/api/json/GenerateCSharpClass",
    method: "post",
    data: input,
  });
};

export const GenerateClassFromJsonByNewtonsoft = (
  input: GenerateCSharpClassInput
) => {
  return request<Md5Output>({
    url: "/api/json/GenerateClassFromJsonByNewtonsoft",
    method: "post",
    data: input,
  });
};
