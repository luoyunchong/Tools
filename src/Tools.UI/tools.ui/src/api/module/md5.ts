import request from '@/utils/axios';

export const allmd5 = (md5input:string)=> {
  return request<Md5Output>({
      url: '/api/md5/all',
      method: 'get',
      params: {sourceString:md5input}
  });
};
