 
<template>
  <n-grid x-gap="12" :cols="2">
    <n-gi>
      <n-card title="Base64 在线编码解码">
        <n-list bordered="false">
          <n-list-item>
            <n-input v-model:value="data.text" type="textarea" placeholder="请输入要进行 Base64 编码或解码的字符" />
          </n-list-item>
          <n-list-item>
            <n-space>
              <n-button type="success" @click="convertEncode">
                Base64编码
              </n-button>
              <n-button type="info" @click="convertDecode">
                Base64解码
              </n-button>
              <n-button @click="exchange">
                交换
              </n-button>
              <n-checkbox v-model:checked="data.ischeck" style="line-height:34px;">URL编码</n-checkbox>
            </n-space>
          </n-list-item>
          <n-list-item>
            <n-input-group>
              <n-input ref="md5Digit16" v-model:value="data.base64" type="textarea" placeholder="转换结果" />
            </n-input-group>
          </n-list-item>
          <n-list-item>
            <n-button type="primary" ghost @click="copy(data.base64)">
              复制结果
            </n-button>
          </n-list-item>
        </n-list>
      </n-card>
    </n-gi>
    <n-gi>
      <div class="green" />
    </n-gi>
  </n-grid>

</template>



<script lang="ts" setup>
import { Base64 } from 'js-base64';
const data = reactive({
  text: '',
  base64: '',
  ischeck:false
})

const convertEncode = () => {
  if (data.ischeck){
    data.base64 = Base64.encodeURI(data.text);
  }else{
    data.base64 = Base64.encode(data.text);
  }
}

const convertDecode = () => {
  data.base64 = Base64.decode(data.text);
}

const exchange = () => {
  const temp = data.text;
  data.text = data.base64;
  data.base64 = temp;
}

const message = useMessage()
const copy = (text: string) => {
  navigator.clipboard.writeText(text);
  message.success("复制成功")
}


</script>