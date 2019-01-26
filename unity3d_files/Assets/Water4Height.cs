// Water4 Height script
// 
// Translated from Unitys Water4 shader files

/*This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain.We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors.We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.*/

/* USAGE:

GetHeightAtPos (Vector3) will return a height for a position.

Unless the Update() function is deleted, any GameObject with this script will float on top of the water.
*/

using UnityEngine;
using System.Collections;

public class Water4Height : MonoBehaviour
{

    public Material Material; // Set this to the Water4Advanced material found in Standard Assets/Enviroment/Water/Water4/Materials

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = GetHeightAtPos(transform.position);
        transform.position = pos;
    }

    float GetHeightAtPos(Vector3 pos)
    {
        Vector3 offset = GerstnerOffset4(
            new Vector2(pos.x, pos.z),
            Material.GetVector("_GSteepness"),                                                // steepness
            Material.GetVector("_GAmplitude"),                                                // amplitude
            Material.GetVector("_GFrequency"),                                                // frequency
            Material.GetVector("_GSpeed"),                                                    // speed
            Material.GetVector("_GDirectionAB"),                                              // direction # 1, 2
            Material.GetVector("_GDirectionCD")                                               // direction # 3, 4
        );

        return offset.y;
    }

    Vector3 GerstnerOffset4(Vector2 xzVtx, Half4 steepness, Half4 amp, Half4 freq, Half4 speed, Half4 dirAB, Half4 dirCD)
    {
        Vector3 offsets;

        Half4 AB = new Half4();
        AB.x = steepness.x * amp.x * dirAB.x;
        AB.y = steepness.x * amp.x * dirAB.y;
        AB.z = steepness.y * amp.y * dirAB.z;
        AB.w = steepness.y * amp.y * dirAB.w;

        Half4 CD = new Half4();
        AB.x = steepness.z * amp.z * dirCD.x;
        AB.y = steepness.z * amp.z * dirCD.y;
        AB.z = steepness.w * amp.w * dirCD.z;
        AB.w = steepness.w * amp.w * dirCD.w;


        Half4 dotABCD = freq * new Half4(Vector2.Dot(dirAB.xy, xzVtx), Vector2.Dot(dirAB.zw, xzVtx), Vector2.Dot(dirCD.xy, xzVtx), Vector2.Dot(dirCD.zw, xzVtx));

        float t = Time.time;
        Half4 TIME = new Half4(t) * speed;

        Half4 COS = Half4.Cos(dotABCD + TIME);
        Half4 SIN = Half4.Sin(dotABCD + TIME);

        offsets.x = Vector4.Dot(COS, new Half4(AB.xz, CD.xz));
        offsets.z = Vector4.Dot(COS, new Half4(AB.yw, CD.yw));
        offsets.y = Vector4.Dot(SIN, amp) * 0.5f;

        return offsets;
    }

    Vector4 V4Mult(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }

    struct Half4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector2 xy
        {
            get { return new Vector2(x, y); }
            private set { }
        }

        public Vector2 xz
        {
            get { return new Vector2(x, z); }
            private set { }
        }

        public Vector2 yw
        {
            get { return new Vector2(y, w); }
            private set { }
        }

        public Vector2 zw
        {
            get { return new Vector2(z, w); }
            private set { }
        }

        public Half4(float xx, float yy, float zz, float ww)
        {
            x = xx;
            y = yy;
            z = zz;
            w = ww;
        }

        public Half4(Vector2 xy, Vector2 zw)
        {
            x = xy.x;
            y = xy.y;
            z = zw.x;
            w = zw.y;
        }

        public Half4(float xx)
        {
            x = xx;
            y = xx;
            z = xx;
            w = xx;
        }

        public static implicit operator Half4(Vector4 v)
        {
            return new Half4(v.x, v.y, v.z, v.w);
        }

        public static implicit operator Vector4(Half4 v)
        {
            return new Vector4(v.x, v.y, v.z, v.w);
        }

        public static Half4 operator *(Half4 a, Half4 b)
        {
            return new Half4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public static Half4 operator +(Half4 a, Half4 b)
        {
            return new Half4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Half4 Sin(Half4 a)
        {
            return new Half4(Mathf.Sin(a.x), Mathf.Sin(a.y), Mathf.Sin(a.z), Mathf.Sin(a.w));
        }

        public static Half4 Cos(Half4 a)
        {
            return new Half4(Mathf.Cos(a.x), Mathf.Cos(a.y), Mathf.Cos(a.z), Mathf.Cos(a.w));
        }
    }
}