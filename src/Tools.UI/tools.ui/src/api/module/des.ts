import request from '@/utils/axios';

export const encrypt = (desInput: DESInput) => {
  return request<string>({
    url: "/api/des/encrypt",
    method: "post",
    data: desInput
  });
};

export const decrypt = (desInput: DESInput) => {
  return request<string>({
    url: "/api/des/decrypt",
    method: "post",
    data: desInput,
  });
};
