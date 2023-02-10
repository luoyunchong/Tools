import request from "@/utils/axios";

interface SM4ECBInput {
  data: string;
  keys: string;
  enOrDecrpyt: boolean;
}

export const EcbPadding = (input: SM4ECBInput) => {
  return request<SM4ECBOutput>({
    url: "/api/sm4/ecb_padding",
    method: "post",
    data: input,
  });
};
